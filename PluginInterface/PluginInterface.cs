using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace PluginInterface
{
    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

       // System.Windows.Forms.UserControl MainInterface { get; }

        void Initialize();
        void Dispose();

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }

    public interface IPluginHost
    {
        void Feedback(string Feedback, IPlugin Plugin);
    }
}
