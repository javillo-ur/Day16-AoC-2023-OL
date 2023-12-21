﻿using System.Collections.Concurrent;

Console.WriteLine(new List<string[]>(){File.ReadAllLines("input.txt")}.Select(j => new ConcurrentDictionary<(int,int), int>(j.SelectMany((row, y) => row.Select((c, x) => (x, y, c))).ToDictionary(t => (t.x, t.y), t => t.c == '.' ? 0 : t.c == '/' ? 1 : t.c == '\\' ? 2 : t.c == '|' ? 3 : 4))).Select(j => new{dict = j,cases = Enumerable.Range(0, j.Keys.Max(t => t.Item1)+1).SelectMany(r => Enumerable.Range(0, 4).Select(dir => (r, dir, new ConcurrentDictionary<(int,int), bool[]>(j.Keys.Select(t => new KeyValuePair<(int, int), bool[]>((t.Item1, t.Item2), [false, false, false, false])))))),height = j.Keys.Max(t => t.Item1)}).Select(j => j.cases.Select(sw => Enumerable.Range(0, 4*j.dict.Keys.Count).Aggregate((new List<(int,int,int)>(){sw.dir == 0 ? (sw.r, -1, 2) : sw.dir == 1 ? (j.height+1, sw.r, 3) : sw.dir == 2 ? (sw.r, j.height+1, 0) : (-1, sw.r, 1)} as IEnumerable<(int,int,int)>, sw.Item3), (previous, current) => !previous.Item1.Any() ? ([], sw.Item3) : (previous.Item1.SelectMany(beam => beam.Item3 == 0 ? (j.dict.ContainsKey((beam.Item1, beam.Item2-1)) ? (j.dict[(beam.Item1, beam.Item2-1)] == 0 ? new List<(int,int,int)>(){(beam.Item1, beam.Item2-1, beam.Item3)} : j.dict[(beam.Item1, beam.Item2-1)] == 1 ? [(beam.Item1, beam.Item2-1, 1)] : j.dict[(beam.Item1, beam.Item2-1)] == 2 ? [(beam.Item1, beam.Item2-1, 3)] : j.dict[(beam.Item1, beam.Item2-1)] == 3 ? [(beam.Item1, beam.Item2-1, 0)] : [(beam.Item1, beam.Item2-1, 1), (beam.Item1, beam.Item2-1, 3)]) : []) : beam.Item3 == 1 ? (j.dict.ContainsKey((beam.Item1+1, beam.Item2)) ? (j.dict[(beam.Item1+1, beam.Item2)] == 0 ? new List<(int,int,int)>(){(beam.Item1+1, beam.Item2, beam.Item3)} : j.dict[(beam.Item1+1, beam.Item2)] == 1 ? [(beam.Item1+1, beam.Item2, 0)] : j.dict[(beam.Item1+1, beam.Item2)] == 2 ? [(beam.Item1+1, beam.Item2, 2)] : j.dict[(beam.Item1+1, beam.Item2)] == 3 ? [(beam.Item1+1, beam.Item2, 0), (beam.Item1+1, beam.Item2, 2)] : [(beam.Item1+1, beam.Item2, 1)]) : []) : beam.Item3 == 2 ? (j.dict.ContainsKey((beam.Item1, beam.Item2+1)) ? (j.dict[(beam.Item1, beam.Item2+1)] == 0 ? new List<(int,int,int)>(){(beam.Item1, beam.Item2+1, beam.Item3)} : j.dict[(beam.Item1, beam.Item2+1)] == 1 ? [(beam.Item1, beam.Item2+1, 3)] : j.dict[(beam.Item1, beam.Item2+1)] == 2 ? [(beam.Item1, beam.Item2+1, 1)] : j.dict[(beam.Item1, beam.Item2+1)] == 3 ? [(beam.Item1, beam.Item2+1, 2)] : [(beam.Item1, beam.Item2+1, 1), (beam.Item1, beam.Item2+1, 3)]) : []) : (j.dict.ContainsKey((beam.Item1-1, beam.Item2)) ?  (j.dict[(beam.Item1-1, beam.Item2)] == 0 ? new List<(int,int,int)>(){(beam.Item1-1, beam.Item2, beam.Item3)} : j.dict[(beam.Item1-1, beam.Item2)] == 1 ? [(beam.Item1-1, beam.Item2, 2)] : j.dict[(beam.Item1-1, beam.Item2)] == 2 ? [(beam.Item1-1, beam.Item2, 0)] : j.dict[(beam.Item1-1, beam.Item2)] == 3 ? [(beam.Item1-1, beam.Item2, 0), (beam.Item1-1, beam.Item2, 2)] : [(beam.Item1-1, beam.Item2, 3)]) : [])).Where(newbeam => !sw.Item3[(newbeam.Item1, newbeam.Item2)][newbeam.Item3]).Select(beam => new{beam, act = sw.Item3.TryGetValue((beam.Item1, beam.Item2), out var value) && sw.Item3.TryUpdate((beam.Item1, beam.Item2), [value[0] || beam.Item3 == 0, value[1] || beam.Item3 == 1, value[2] || beam.Item3 == 2, value[3] || beam.Item3 == 3], value)}).ToList().Select(j => j.beam), sw.Item3)))).First().Max(t => t.Item2.Keys.Where(key => t.Item2[key].Any(w => w)).Count()));