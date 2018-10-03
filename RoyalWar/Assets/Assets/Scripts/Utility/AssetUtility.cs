namespace RoyalWar
{
    public static class AssetUtility
    {
        public static string GetConfigAsset(string assetName)
        {
            return string.Format("Assets/Assets/Configs/{0}.txt", assetName);
        }

        public static string GetDataTableAsset(string assetName)
        {
            return string.Format("Assets/Assets/DataTables/{0}.txt", assetName);
        }

        public static string GetDictionaryAsset(string assetName)
        {
            return string.Format("Assets/Assets/Localization/{0}/Dictionaries/{1}.xml", GameEntry.Localization.Language.ToString(), assetName);
        }

        public static string GetFontAsset(string assetName)
        {
            return string.Format("Assets/Assets/Localization/{0}/Fonts/{1}.ttf", GameEntry.Localization.Language.ToString(), assetName);
        }

        public static string GetSceneAsset(string assetName)
        {
            return string.Format("Assets/Assets/Scenes/{0}.unity", assetName);
        }

        public static string GetMusicAsset(string assetName)
        {
            return string.Format("Assets/Assets/Music/{0}.mp3", assetName);
        }

        public static string GetSoundAsset(string assetName)
        {
            return string.Format("Assets/Assets/Sounds/{0}.wav", assetName);
        }

        public static string GetEntityAsset(string assetName)
        {
            return string.Format("Assets/Assets/Entities/{0}.prefab", assetName);
        }

        public static string GetUIFormAsset(string assetName)
        {
            return string.Format("Assets/Assets/UI/UIForms/{0}.prefab", assetName);
        }

        public static string GetUISoundAsset(string assetName)
        {
            return string.Format("Assets/Assets/UI/UISounds/{0}.wav", assetName);
        }
    }
}
