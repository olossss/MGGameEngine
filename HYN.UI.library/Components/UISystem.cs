#region Using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using HYM.System.library;

#endregion
namespace HYM.UI.library
{
    [ArtemisEntitySystem( GameLoopType = GameLoopType.Update, Layer = 4)]
    public class UISystem : EntityComponentProcessingSystem<MouseStateComponent,NameComponent, RectangleComponents>
    {
        
        //<summary>The sprite batch.</summary>
        private SpriteBatch spriteBatch;
        public override void LoadContent()
        {
            this.spriteBatch = EntitySystem.BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
        }
        public override void Process(Entity entity, MouseStateComponent m_MouseStateComponent,NameComponent modelComponent, RectangleComponents transformComponent)
        {
            
            m_MouseStateComponent.CurrentMouseState = Mouse.GetState();
            if ((m_MouseStateComponent.CurrentMouseState.LeftButton == ButtonState.Released) && (m_MouseStateComponent.LastMouseState.LeftButton == ButtonState.Pressed))
            {
                    //entity.GetComponent<TextComponent>().TextComponentFile = "2";
                    Rectangle rect = new Rectangle(transformComponent.RectangleFile.X - (transformComponent.RectangleFile.Width / 2),
                            transformComponent.RectangleFile.Y - (transformComponent.RectangleFile.Height / 2),
                            transformComponent.RectangleFile.Width, transformComponent.RectangleFile.Height);
                    if (rect.Contains(m_MouseStateComponent.CurrentMouseState.X, m_MouseStateComponent.CurrentMouseState.Y))
                    {
                        GameEvent.Event_Button_Click(modelComponent.Name);
                    }
            }
            m_MouseStateComponent.LastMouseState = m_MouseStateComponent.CurrentMouseState;
        }
    }
}
