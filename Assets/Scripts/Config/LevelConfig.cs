using Newtonsoft.Json;

public class LevelConfig
{
    [JsonProperty("levels")]
    public LevelData[] Levels;
}

public class LevelData
{
    [JsonProperty("zone_scene")]
    public string ZoneScene;

    [JsonProperty("stages")]
    public StageConfig[] Stages;

}

public class StageConfig
{
    [JsonProperty("enemy_count")]
    public int EnemyCount;
}