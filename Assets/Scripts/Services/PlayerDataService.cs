using System;
using UnityEngine;


namespace SpaceRogue.Services
{
    public sealed class PlayerDataService : IDisposable
    {
        private const string RecordKey = "RecordCompletedLevels";
        
        public int CompletedLevels { get; private set; } = 0;
        public int RecordCompletedLevels { get; private set; } = 0;

        public PlayerDataService()
        {
            RecordCompletedLevels = PlayerPrefs.GetInt(RecordKey);
        }

        public void ResetRecord()
        {
            CompletedLevels = 0;
            RecordCompletedLevels = 0;
        }

        public void Dispose()
        {
            PlayerPrefs.SetInt(RecordKey, RecordCompletedLevels);
        }
    }
}