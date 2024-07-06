﻿using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.HitObjectParser;
using osuToolsV2.Exceptions;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.LazyLoaders;

public class HitObjectLazyLoader : ILazyLoader<List<IHitObject>>
{
    private readonly IEnumerable<string> _hitObjectDefinitions;
    private readonly Beatmap _beatmap;
    private List<IHitObject>? _cache;
    private ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();

    public HitObjectLazyLoader(IEnumerable<string> hitObjectDefinitions, Beatmap beatmap)
    {
        _hitObjectDefinitions = hitObjectDefinitions;
        _beatmap = beatmap;
    }

    public bool Loaded { get; private set; }
    public bool Loading { get; private set; }

    public List<IHitObject> LoadObject()
    {
        if (_cache != null)
        {
            return _cache;
        }
        
        try
        {
            Loading = true;
            _readerWriterLock.EnterWriteLock(); 
            return LoadObjectNoLock();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _readerWriterLock.ExitWriteLock();
        }
    }

    List<IHitObject> LoadObjectNoLock()
    {
        List<IHitObject> hitObjects = new List<IHitObject>();
        foreach (var line in _hitObjectDefinitions)
        {
            IHitObjectCreator hitObjectCreator = _beatmap.Ruleset.LegacyRuleset switch
            {
                LegacyRuleset.Osu => new OsuHitObjectCreator(),
                LegacyRuleset.Taiko => new TaikoHitObjectCreator(),
                LegacyRuleset.Catch => new CatchHitObjectCreator(),
                LegacyRuleset.Mania => new ManiaHitObjectCreator(),
                LegacyRuleset.None => throw new InvalidBeatmapException(),
                _ => throw new InvalidBeatmapException()
            };

            if (hitObjectCreator == null)
            {
                throw new InvalidBeatmapException();
            }

            hitObjects.Add(hitObjectCreator.CreateHitObject(line.Split(','), _beatmap));
        }

        Loaded = true;
        Loading = false;
        _cache = hitObjects;
        
        return hitObjects;
    }
}