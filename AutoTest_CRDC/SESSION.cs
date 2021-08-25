using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest_CRDC
{
    public class SESSION
    {
        private const string WindowsApplicationDriverUrl = "http://www.google.com.br";//Ou pode usar o WindowsApplication Driver e ficaria = "http://127.0.0.1:4723/"
        //protected static WindowsDriver<WindowsElement> session;
        protected static IWebDriver session = new ChromeDriver();
        public static void Setup()
        {
            //Se a sessão não iniciou vai iniciar aqui
            //if (session == null)
            //{

                session.Navigate().GoToUrl("https://plataforma.crdc.com.br:8443/");
                //faz a instancia da aplicação nesse caso do Google Chrome

            //}
        }

        public static void FecharAPP()
        {
            //Caso a aplicação esteja aberta vai fechar
            if (session != null)
            {
                session.Quit();
                session = null;
            }
        }
    }
}
