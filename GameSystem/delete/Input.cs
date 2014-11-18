using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameSystem
{
    class CInput
    {
        #region Keyboard
        //The recent keyboard state to compare with the current one
        protected KeyboardState recentKeyboardState;
        //Two events for the keyboard
        public delegate void OnKeyDownDelegate(Keys k);
        public delegate void OnKeyUpDelegate(Keys k);
        public event OnKeyDownDelegate OnKeyDown;
        public event OnKeyUpDelegate OnKeyUp;
        #endregion

        #region Mouse
        //The recent mouse state to compare with the current one
        protected MouseState recentMouseState;
        //an enum for all the buttons
        public enum MouseButtons
        { 
            LeftButton,
            MiddleButton,
            RightButton,
            XButton1,
            XButton2
        }
        //Two events for the mouse buttons
        public delegate void OnMouseDownDelegate(MouseButtons b);
        public delegate void OnMouseUpDelegate(MouseButtons b);
        public event OnMouseDownDelegate OnMouseDown;
        public event OnMouseUpDelegate OnMouseUp;
        //An event for the mouse movement
        public delegate void OnMouseMoveDelegate(Vector2 p, Vector2 d);
        public event OnMouseMoveDelegate OnMouseMove;
        //An event for the scroll wheel
        public delegate void OnMouseScrollDelegate(int s, int d);
        public event OnMouseScrollDelegate OnMouseScroll;
        #endregion

        public CInput()
        {
            //We need something initially
            recentKeyboardState = Keyboard.GetState();
            recentMouseState = Mouse.GetState();
            
        }

        public void Update()
        {
            UpdateKeyboard();
            UpdateMouse();
        }

        protected void UpdateKeyboard()
        {
            //Get the recent and collection of pressed keys
            List<Keys> recent = new List<Keys>(recentKeyboardState.GetPressedKeys());
            //Get the current keyboard state
            recentKeyboardState = Keyboard.GetState();
            List<Keys> current = new List<Keys>(recentKeyboardState.GetPressedKeys());
            //Remove common elements because we don't need to deal with them
            RemoveCommonElements<Keys>(recent, current);
            //Call the events
            //All the keys that are in the recent collection have been released
            foreach (Keys k in recent)
                if(OnKeyUp != null) OnKeyUp(k);
            //All the keys that are in the current collection have been pressed
            foreach (Keys k in current)
                if(OnKeyDown != null) OnKeyDown(k);
        }

        protected void UpdateMouse()
        {
            //Get the current mouse state
            MouseState currentMouseState = Mouse.GetState();
            #region Mouse Buttons
            //Go through all the buttons by creating something like a key collection
            List<MouseButtons> recent = new List<MouseButtons>();
            //Add all the buttons that were pressed in the recent state
            if (recentMouseState.LeftButton == ButtonState.Pressed)
                recent.Add(MouseButtons.LeftButton);
            if (recentMouseState.MiddleButton == ButtonState.Pressed)
                recent.Add(MouseButtons.MiddleButton);
            if (recentMouseState.RightButton == ButtonState.Pressed)
                recent.Add(MouseButtons.RightButton);
            if (recentMouseState.XButton1 == ButtonState.Pressed)
                recent.Add(MouseButtons.XButton1);
            if (recentMouseState.XButton2 == ButtonState.Pressed)
                recent.Add(MouseButtons.XButton2);
            //Create the same list for the current state
            List<MouseButtons> current = new List<MouseButtons>();
            //Add all the buttons that are currently pressed
            if (currentMouseState.LeftButton == ButtonState.Pressed)
                current.Add(MouseButtons.LeftButton);
            if (currentMouseState.MiddleButton == ButtonState.Pressed)
                current.Add(MouseButtons.MiddleButton);
            if (currentMouseState.RightButton == ButtonState.Pressed)
                current.Add(MouseButtons.RightButton);
            if (currentMouseState.XButton1 == ButtonState.Pressed)
                current.Add(MouseButtons.XButton1);
            if (currentMouseState.XButton2 == ButtonState.Pressed)
                current.Add(MouseButtons.XButton2);
            //Remove all the common items as they don't need to be dealt with
            RemoveCommonElements<MouseButtons>(recent, current);
            //Call all the methods
            //Those were down
            foreach (MouseButtons b in recent)
                if(OnMouseUp != null) OnMouseUp(b);
            //Those are down now
            foreach (MouseButtons b in current)
                if(OnMouseDown != null) OnMouseDown(b);
            #endregion
            #region Mouse Position
            //If any of the two positions changed...
            if ((currentMouseState.X != recentMouseState.X || currentMouseState.Y != recentMouseState.Y)&&OnMouseMove != null)
                //...call the event handler and pass on the current position and the change in position
                OnMouseMove(new Vector2(currentMouseState.X, currentMouseState.Y), new Vector2(currentMouseState.X - recentMouseState.X,
                    currentMouseState.Y - recentMouseState.Y));
            #endregion
            #region Mouse Scroll
            //If the value changed...
            if (currentMouseState.ScrollWheelValue != recentMouseState.ScrollWheelValue && OnMouseScroll != null)
                //...call the event handler and pass on the current value and the change in value
                OnMouseScroll(currentMouseState.ScrollWheelValue, currentMouseState.ScrollWheelValue - recentMouseState.ScrollWheelValue);
            #endregion
            //Update it for the next iteration
            recentMouseState = currentMouseState;
        }

        protected void RemoveCommonElements<ElementType>(List<ElementType> l1, List<ElementType> l2)
        {
            //Go through all the items in the first list
            for (int i = 0; i < l1.Count; i++)
            { 
                //if the second list contains the current item
                if (l2.Contains(l1[i]))
                {
                    //Remove the second list first
                    l2.Remove(l1[i]);
                    //and then from the first one
                    l1.RemoveAt(i);
                    //decrement once because we have to recheck that index
                    i--;
                }
            }
        }
    }
}
