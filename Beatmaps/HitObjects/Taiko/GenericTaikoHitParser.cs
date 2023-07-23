// using osuToolsV2.Beatmaps.HitObjects.Osu;
// using osuToolsV2.Beatmaps.HitObjects.Sounds;
//
// namespace osuToolsV2.Beatmaps.HitObjects.Taiko;
//
// public static class GenericTaikoHitParser
// {
//     public static IHitObject Parse(string[] data)
//     {
//         return ParseTaikoHit(data);
//     }
//     
//     public static IHitObject ParseTaikoHit(string[] data)
//     {
//         ITaikoHit taikoHit;
//         HitCircle circle = new HitCircle();
//         HitObjectTools.GenericParse(circle, data, out var oriType);
//         if (!oriType.HasFlag(OriginalHitObjectType.HitCircle))
//         {
//             return ParseSlider(data);
//         }
//         circle.Parse(data);
//         bool isWhistle = (circle.HitSound & HitSound.Whistle) != 0;
//         bool isClap = (circle.HitSound & HitSound.Clap) != 0;
//         bool isWFinish = (circle.HitSound & HitSound.Finish) != 0;
//         if (isWhistle || isClap)
//         {
//             taikoHit = isWFinish
//                 ? new TaikoLargeBlueHit {OriginalHitObjectType = oriType}
//                 : new TaikoBlueHit {OriginalHitObjectType = oriType};
//         }
//         else
//         {
//             taikoHit = isWFinish
//                 ? new TaikoLargeRedHit(){OriginalHitObjectType = oriType}
//                 : new TaikoRedHit(){OriginalHitObjectType = oriType};
//         }
//
//         taikoHit.Position = circle.Position;
//         taikoHit.HitSample = circle.HitSample;
//         taikoHit.HitSound = circle.HitSound;
//         taikoHit.StartTime = circle.StartTime;
//         return taikoHit;
//     }
//
//     
//     public static IHitObject ParseSlider(string[] data)
//     {
//         Slider slider = new Slider();
//         HitObjectTools.GenericParse(slider,data,out var oriType);
//         
//         if (!oriType.HasFlag(OriginalHitObjectType.Slider))
//         {
//             return ParseSpinner(data);
//         }
//         slider.Parse(data);
//         
//         
//         return new Drumroll()
//         {
//             StartTime = slider.StartTime,
//             Length = slider.Length,
//             OriginalHitObjectType = oriType
//         };
//     }
//
//     public static IHitObject ParseSpinner(string[] data)
//     {
//         Spinner spinner = new Spinner();
//         HitObjectTools.GenericParse(spinner, data, out var oriType);
//         if (!oriType.HasFlag(OriginalHitObjectType.Spinner))
//         {
//             throw new InvalidOperationException($"Can not process type {oriType}");
//         }
//         spinner.Parse(data);
//         IHitObject denden = new DenDen()
//         {
//             StartTime = spinner.StartTime,
//             EndTime = spinner.EndTime,
//             OriginalHitObjectType = oriType
//         };
//         return denden;
//     }
// }