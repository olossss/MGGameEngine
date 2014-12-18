using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYM.System.library
{
    public delegate void UI(string Name);
    public delegate void System();

    public static class GameEvent
    {
        public static event UI Button_Click;
        public static void Event_Button_Click(string Name)
        {
            if (Button_Click != null)  //在这里触发事件之前对Myevent进行检查，可以避免NullReferenceException的发生。（调用一个委托之前，要检查它的值是不是空值。）  
            {
                Button_Click(Name);
            }
        }
        public static event System Quit;
        public static void Event_Quit()
        {
            if (Quit != null)  //在这里触发事件之前对Myevent进行检查，可以避免NullReferenceException的发生。（调用一个委托之前，要检查它的值是不是空值。）  
            {
                Quit();
            }
        }
    }
}
