// Type: Microsoft.Xna.Framework.Content.ContentManager
// Assembly: Microsoft.Xna.Framework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553
// Assembly location: C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0\References\Windows\x86\Microsoft.Xna.Framework.dll

using System;
using System.IO;

namespace Microsoft.Xna.Framework.Content
{
    public class ContentManager : IDisposable
    {
        public ContentManager(IServiceProvider serviceProvider);
        public ContentManager(IServiceProvider serviceProvider, string rootDirectory);
        public IServiceProvider ServiceProvider { get; }
        public string RootDirectory { get; set; }

        #region IDisposable Members

        public void Dispose();

        #endregion

        protected virtual void Dispose(bool disposing);
        public virtual void Unload();
        public virtual T Load<T>(string assetName);
        protected T ReadAsset<T>(string assetName, Action<IDisposable> recordDisposableObject);
        protected virtual Stream OpenStream(string assetName);
    }
}
