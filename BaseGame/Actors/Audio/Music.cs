using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Actors;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BaseGame.Audio
{
    public class Music:BaseActor,INeedsResources
    {
        private readonly string _songPath;
        protected Song Song;
        public Music(string songPath)
        {
            _songPath = songPath;
        }

        protected override void ResolveResources()
        {
            Song = ServiceLocator.Current.GetInstance<Song>(_songPath); ;
        }

        public IEnumerable<Resource> ResourcePaths()
        {
            return new Resource[]{new Resource(_songPath,typeof(Song)), };
        }
        public void Play()
        {
            ResolveResourcesIfNeeded();
            MediaPlayer.Play(Song);
            MediaPlayer.IsRepeating = true;
        }
        public void Stop()
        {
            MediaPlayer.Stop();
        }
    }
}
