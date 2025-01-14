﻿using System.Runtime.InteropServices;

namespace YSAutoPlayer.Core
{
    public class KeyBoardPlayer : IPlayer
    {
        private readonly IReadOnlyDictionary<Note, ushort> NoteKeyCodeMap = new Dictionary<Note, ushort>()
        {
            [Note.DoLow] = 0x5A,// Z
            [Note.ReLow] = 0x58,// X
            [Note.MiLow] = 0x43,// C
            [Note.FaLow] = 0x56,// V
            [Note.SolLow] = 0x42,// B
            [Note.LaLow] = 0x4E,// N
            [Note.SiLow] = 0x4D,// M
            [Note.Do] = 0x41,// A
            [Note.Re] = 0x53,// S
            [Note.Mi] = 0x44,// D
            [Note.Fa] = 0x46,// F
            [Note.Sol] = 0x47,// G
            [Note.La] = 0x48,// H
            [Note.Si] = 0x4A,// J
            [Note.DoHigh] = 0x51,// Q
            [Note.ReHigh] = 0x57,// W
            [Note.MiHigh] = 0x45,// E
            [Note.FaHigh] = 0x52,// R
            [Note.SolHigh] = 0x54,// T
            [Note.LaHigh] = 0x59,// Y
            [Note.SiHigh] = 0x55,// U
        };

        async Task IPlayer.PlayAsync(MusicScore musicScore, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = new List<Task>();
                // 每拍所占时长
                var tick = 60000 / musicScore.Beat;
                var trackInOne = new SortedDictionary<double, IList<Note>>();
                foreach (var track in musicScore.Tracks)
                {
                    var current = 0d;
                    foreach (var note in track)
                    {
                        current += note.Value;
                        cancellationToken.ThrowIfCancellationRequested();
                        if (!trackInOne.TryGetValue(current, out var notes))
                        {
                            notes = new List<Note>();
                            trackInOne[current] = notes;
                        }
                        notes.Add(note.Key);
                    }
                }
                var last = 0d;
                foreach (var notes in trackInOne)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var delay = (int)(tick * (notes.Key - last));
                    await Task.WhenAll(notes.Value.Distinct().Select(note => Task.Run(() =>
                    {
                        if (note != Note.Zero)
                        {
                            var code = NoteKeyCodeMap[note];
                            PressKey(code);
                        }
                    }, cancellationToken)));
                    await Task.Delay(delay, cancellationToken);
                    last = notes.Key;
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private static void PressKey(ushort code)
        {
            var scan = (byte)MapVirtualKey(code, 0);
            keybd_event(code, scan, 1, 0);
            keybd_event(code, scan, 1 | 2, 0);
        }

        #region WIN32API

        [DllImport("user32.dll", EntryPoint = "MapVirtualKey")]
        private extern static uint MapVirtualKey(ushort uCode, uint uMapType);
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        private extern static void keybd_event(ushort bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        #endregion
    }
}
