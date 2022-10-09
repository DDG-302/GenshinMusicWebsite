using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Midi;
using System.Threading;


namespace genshinmusic
{
    class MIDIPlayer
    {
        const int instrument = 0; // piano
        const int midi_file_type = 0; // 0表示单音轨
        const int track_num = 0;   // 轨道数 
        const int channel_num = 1; // 单通道
        const int tick_per_quarter_note = 120;
        static int[] keyidx_2_midi_val = new int[7] {11, 9, 7, 5, 4, 2, 0}; // 键盘索引到midi值
        private int octave = 3; // 八度偏移
        
        private Task play_task;
        private ManualResetEvent play_or_pause = new ManualResetEvent(true);
        private ManualResetEvent stop_play_event = new ManualResetEvent(false);
        
        public enum PlayStatus { PLAYING, PAUSE, STOP };
        public PlayStatus current_status = PlayStatus.STOP;
        private static bool is_device_on = false;
        static MidiOut midiOut = new MidiOut(0);
        public delegate void Stop_signal();
        public event Stop_signal music_reach_to_final;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key_idx">键盘索引</param>
        /// <param name="octave">八度值</param>
        /// <returns></returns>
        private int change_to_note_val(int key_idx, int eight_degree_num)
        {
            int midi_val = keyidx_2_midi_val[key_idx % 7] + (eight_degree_num - key_idx / 7 + octave) * 12;
            return midi_val;
        }

        /// <summary>
        /// 直接通过note获得midi值
        /// </summary>
        /// <param name="note"></param>
        /// <param name="octave"></param>
        /// <returns></returns>
        private int change_to_note_val(Note note, int eight_degree_num)
        {
            int midi_val = keyidx_2_midi_val[note.Key_idx % 7] + (eight_degree_num - note.Key_idx / 7 + octave) * 12;
            return midi_val;
        }

        public bool create_midi_file(List<Note> music_sheet, int speed, string path, int eight_degree_num)
        {
            try
            {
                int midi_bpm = speed; // 相当于16分音符为1拍，虽然与
                var collection = new MidiEventCollection(midi_file_type, tick_per_quarter_note);
                long absolute_time = 0;
                collection.AddEvent(new TextEvent("Note Stream", MetaEventType.TextEvent, absolute_time), track_num);
                absolute_time++;
                collection.AddEvent(new TempoEvent(60 * 1000 * 1000 / midi_bpm, absolute_time), track_num);
                collection.AddEvent(new PatchChangeEvent(0, channel_num, instrument), track_num);
                int note_velocity = 100;
                int note_duration = tick_per_quarter_note;
                foreach (var note in music_sheet)
                {
                    absolute_time = note.Absolute_semi_offset * tick_per_quarter_note / 4;
                    int note_val = change_to_note_val(note, eight_degree_num);
                    collection.AddEvent(new NoteOnEvent(absolute_time, channel_num, note_val, note_velocity, 1), track_num);
                    collection.AddEvent(new NoteEvent(absolute_time + note_duration * note.Continuous_semi/4, channel_num, MidiCommandCode.NoteOff, note_val, 0), track_num);
                    //collection.AddEvent(new NoteEvent(absolute_time, channel_num, MidiCommandCode.KeyAfterTouch, note_val, 122), track_num);
                }
                collection.PrepareForExport();
                MidiFile.Export(path, collection);
            }
            catch(Exception e)
            {
                LogMsg logMsg = new LogMsg(e);
                ProgramLog.write_log(logMsg);
                return false;
            }

            return true;
        }

        public void set_midi_player(List<Note> music_sheet, int speed, int start_semiquaver, int eight_degree_num)
        {
            if(music_sheet == null)
            {
                current_status = PlayStatus.STOP;
                music_reach_to_final();
                is_device_on = false;
            }
            if (is_device_on == true)
            {
                return;
            }
            is_device_on = true;
            int start_note_idx = 0; // 判断应当开始演奏的音符索引
            for(int i = 0; i < music_sheet.Count; i++)
            {
                if(music_sheet[i].Absolute_semi_offset >= start_semiquaver)
                {
                    start_note_idx = i;
                    break;
                }

            }
            play_task = new Task(() =>
            {
                try
                {
                    int pre_start_semiquaver = start_semiquaver;
                    float quarter_time_milis = 60.0f / speed / 4f * 1000.0f;
                    //midiOut = new MidiOut(0);
                    midiOut.Send(MidiMessage.ChangePatch(instrument, 1).RawData);
                    for (int i = start_note_idx; i < music_sheet.Count; i++)
                    {
                        var note = music_sheet[i];
                        while (!play_or_pause.WaitOne(1) && !stop_play_event.WaitOne(1)) ;
                        int duration = (int)((float)(note.Absolute_semi_offset - pre_start_semiquaver) * quarter_time_milis);
                        if (stop_play_event.WaitOne(duration))
                        {
                            break;
                        }

                        for (int j = i; j < music_sheet.Count && music_sheet[j].Absolute_semi_offset == note.Absolute_semi_offset;j++)
                        {
                            note = music_sheet[j];
                            int note_val = change_to_note_val(note, eight_degree_num);
                            midiOut.Send(MidiMessage.StartNote(note_val, 127, 1).RawData);
                            i = j;
                        }
                        pre_start_semiquaver = note.Absolute_semi_offset;

                    }
                    stop_play_event.WaitOne(1500);
            }
            catch(Exception e)
            {
                    LogMsg logMsg = new LogMsg(e);
                    ProgramLog.write_log(logMsg);
                Console.WriteLine("play error!");
            }
            finally
            {
                current_status = PlayStatus.STOP;
                    music_reach_to_final();
                    is_device_on = false;
            }
        });
        }

        public void start_play()
        {
            current_status = PlayStatus.PLAYING;
            play_or_pause.Set();
            play_task.Start();
            stop_play_event.Reset();
        }

        /// <summary>
        /// 继续播放
        /// </summary>
        public void continue_play()
        {
            current_status = PlayStatus.PLAYING;
            play_or_pause.Set();
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public void pause_play()
        {
            current_status = PlayStatus.PAUSE;
            play_or_pause.Reset();
        }

        /// <summary>
        /// 终止播放
        /// </summary>
        public void stop_play()
        {
            stop_play_event.Set();
        }

        /// <summary>
        /// 在键盘上演奏一个音
        /// </summary>
        /// <param name="key_idx">按键值</param>
        /// <param name="eight_degree_num">八度值</param>
        public void play_on_keyboard(int key_idx, int eight_degree_num)
        {
            int note_val = change_to_note_val(key_idx, eight_degree_num);
            midiOut.Send(MidiMessage.StartNote(note_val, 127, 1).RawData);
        }
    }
}
