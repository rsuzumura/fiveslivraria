using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace FivesLivraria
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            EO.Web.Runtime.AddLicense(
            "WLiJWZekscufWZfA8g/jWev9ARC8W8Tp/yChWe3pAx7oqOXBs+StaZmkwOmM" +
            "Q5ekscufWZekzQzjnZf4ChvkdpnX/RTjnsTp/yChWe3pAx7oqOXBs+StaZmk" +
            "wOmMQ5ekscufWZekzQzjnZf4ChvkdpnY8g3SrentAc2fr9z2BBTup7Smytmv" +
            "W5ezz7iJWZekscufWZfA8g/jWev9ARC8W8v29hDVotz7s8v1nun3+hrtdpm9" +
            "v9uhWabCnrWfWZekscufWbPl9Q+frfD09uihhuzwBRTPmt7ps8v1nun3+hrt" +
            "dpm9v9uhWabCnrWfWZekscufWbPl9Q+frfD09uihfNjw9hnjmummsSHkq+rt" +
            "ABm8W7Cywc2faLWRm8ufWZekscufddjo9cvzsufpzs3CmuPw8wzipJmkBxDx" +
            "rODz/+ihcqW0s8uud4SOscufWZekscu7mtvosR/4qdzBs+zJes/ZARfumtvp" +
            "A82fr9z2BBTup7SmytmvW5ezz7iJWZekscufWZfA8g/jWev9ARC8W7vt8hfu" +
            "oJmkBxDxrODz/+ihcqW0s8uud4SOscufWZekscu7mtvosR/4qdzBs/7vpeD4" +
            "BRDxW5f69h3youbyzs24Z6emsdq9RoGkscufWZeksefgndukBSTvnrSm3gzy" +
            "pNzo1g/orZmkBxDxrODz/+ihcqW0s8uud4SOscufWZekscu7mtvosR/4qdzB" +
            "s/LxotumsSHkq+rtABm8W7Cywc2faLWRm8ufWZekscufddjo9cvzsufpzs3C" +
            "qOPzA/vonOLpA82fr9z2BBTup7SmytmvW5ezz7iJWZekscufWZfA8g/jWev9" +
            "ARC8W8r09hfrfN/p9Bbkq5mkBxDxrODz/+ihcqW0s8uud4SOscufWZekscu7" +
            "mtvosR/4qdzBs/DjouvzA82fr9z2BBTup7SmytmvW5ezz7iJWZekscufWZfA" +
            "8g/jWev9ARC8W8Dx8hLkk+bz/s2fr9z2BBTup7SmytmvW5ezz7iJWZekscuf" +
            "WZfA8g/jWev9ARC8W7vzCBnrqNjo9h2hWe3pAx7oqOXBs+StaZmkwOmMQ5ek" +
            "scufWZekzQzjnZf4ChvkdpnK/Rrgrdz2s8v1nun3+hrtdpm9v9uhWabCnrWf" +
            "WZekscufWbPl9Q+frfD09uihjOPt9RChWe3pAx7oqOXBs+StaZmkwOmMQ5ek" +
            "scufWZekzQzjnZf4ChvkdpnK/STuruumsSHkq+rtABm8W7Cywc2faLWRm8uf" +
            "WZekscufddjo9cvzsufpzs3EneD48g3rnsPl8xDrW5f69h3youbyzs24Z6em" +
            "sdq9RoGkscufdabl/RfusLWRm8ufWZfAAB3jnunN/xHuWdvlBRC8W6yzwuKu" +
            "a6e1ws2fr9z2BBTup7Smyc2faLWRm8ufWZfABBTmp9j4Bh3kd8fw5ey0rfDp" +
            "6NrDad/20/b4sOHXwhK8drOzBBTmp9j4Bh3kd4SOzdrrotrp/x7kd4SOdePt" +
            "9BDtrNzCnrWfWZekzRfonNzyBBDInbW3x961aqy8xuC2dabw+g7kp+rp2g+9" +
            "RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzrpeb7zw==");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}