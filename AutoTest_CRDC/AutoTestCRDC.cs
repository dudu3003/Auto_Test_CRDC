using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AutoTest_CRDC
{
    [TestClass]
    public class AutoTestCRDC : SESSION
    {
        public List<ListOperacoes> list;

        public string 
            op = "", verificarMensagem = "";

        public bool 
            agenteFinanceiro = false, emitente = false, avalista = false;

        #region Mensagens Padronizadas
        string mensagemUploadOK = "Upload efetuado com sucesso";
        string mensagemExNenhumPapelSelecionado = "Selecione ao menos um papel para assinatura";
        string mensagemExRepresentanteNaoCadastrado = "não existem representantes cadastrados para o Emitente ou AF";
        #endregion
        #region Dados Padrões para acesso aos testes
        [ClassInitialize]
        public static void Inicializar(TestContext context)
        {
            //Caso necessite iniciar algum processo do Banco de Dados, creio que o ideal
            //seja colocar no inicio do processo de teste
        }

        [ClassCleanup()]
        public static void Finalizar()
        {
            FecharAPP();
        }

        public void AcessarWebCRDC()
        {
            //Configurações padrões para acessar o site.
            //Normalmente usado Login e Senha, porém pode ser usado a Certificação Digital
            //Link para acesso de mesmo nome no Google: https://plataforma.crdc.com.br:8443/
            Setup();
        }

        public void AcessarMenuGestaoDeOperacoes(string op)
        {
            //Nesse método vai acessar o processo para navegar internamente
            //até chegar no menu "Gestão de Operações"
            //Nesse mesmo método fara a busca do processo selecionando a operação desejada
            list = ListOperacoes();
            for (int i = 0; i < 5; i++)//Simulação de opções 5 exemplo para verificar a string op
            {                
                if (list[i].NAME == op)
                {
                    //Caso a opção seja a que recebeu ele vai fazer a interação nesse ponto
                    //e selecionar a opção
                    break;
                }
            }
        }

        public List<ListOperacoes> ListOperacoes()
        {
            List<ListOperacoes> list = new List<ListOperacoes>
            {
                new ListOperacoes
                {
                    NAME = "OPÇÃO 1"
                },
                new ListOperacoes
                {
                    NAME = "OPÇÃO 2"
                },
                new ListOperacoes
                {
                    NAME = "OPÇÃO 3"
                },
                new ListOperacoes
                {
                    NAME = "OPÇÃO 4"
                },
                new ListOperacoes
                {
                    NAME = "OPÇÃO 5"
                }
            };
            return list;
        }

        public void AnexarDocumento()
        {
            //Aqui fará a interação para anexar.
            //simulando que seja por ID isso e o nome dele fosse btnAnexar
            session.FindElement(By.Id("btnAnexar")).Click();
            //FindElementByAccessibilityId("btnAnexar").Click();
            //Provavelmente abrirá um popup pedindo para selecionar o documento
            EncontrarDocumento("NOME DO DOCUMENTO");
        }

        public void EncontrarDocumento(string nomeDoDocumento)
        {
            //Aqui fará a interação para encontrar o documento
        }

        public void ProcessoDeUpload()
        {
            //Aqui vai fazer o clique no botão de upload
            //simulando que o ID é "upload"
            session.FindElement(By.Id("upload")).Click();
        }

        public void SelecionarPapeisParaAssinatura(bool agenteFinanceiro, bool emitente, bool avalista)
        {
            if (agenteFinanceiro)
            {
                //caso seja true para ele vai clicar na opção dele, simulando id "agFinanceiro"
                session.FindElement(By.Id("agFinanceiro")).Click();
            }
            if (emitente)
            {
                //caso seja true para ele vai clicar na opção dele, simulando id "emitente"
                session.FindElement(By.Id("emitente")).Click();
            }
            if (avalista)
            {
                //caso seja true para ele vai clicar na opção dele, simulando id "avalista"
                session.FindElement(By.Id("avalista")).Click();
                //No caso do Avalista estou "imaginando" que abriria uma caixa com uma seleção para
                //colocar os dados do avalista que fará o processo, dessa forma vou imaginar que é uma tela
                //e fará "X" cliques dependendo da opção que escolher, nesse caso o teste não está 100%


            }
        }
        #endregion
        #region Testes
        [TestMethod]
        public void TC001_AcessarMenuGestaoDeOperacoes()
        {
            op = "OPÇÃO 1";//Dado ficticio
            AcessarWebCRDC();
            AcessarMenuGestaoDeOperacoes(op);
            FecharAPP();
        }
        [TestMethod]
        public void TC002_AcessarSiteSelecionandoAgenteFinanceiro()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                SelecionarPapeisParaAssinatura(true, false, false);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemUploadOK));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC003_AcessarSiteSelecionandoEmitente()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                SelecionarPapeisParaAssinatura(false, true, false);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemUploadOK));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC004_AcessarSiteSelecionandoAvalista()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                SelecionarPapeisParaAssinatura(false, false, true);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemUploadOK));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC005_AcessarSiteSelecionandoAgenteFinanceiroEEmitente()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                SelecionarPapeisParaAssinatura(true, true, false);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemUploadOK));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC006_AcessarSiteSelecionandoAgenteFinanceiroEAvalista()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                SelecionarPapeisParaAssinatura(true, false, true);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemUploadOK));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC007_AcessarSiteSelecionandoEmitenteEAvalista()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                SelecionarPapeisParaAssinatura(false, true, true);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemUploadOK));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC008_AcessarSiteSelecionandoAgenteFinanceiroEEmitenteEAvalista()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                SelecionarPapeisParaAssinatura(true, true, true);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemUploadOK));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC009_AcessarSiteNaoSelecionandoPapeis()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                SelecionarPapeisParaAssinatura(false, false, false);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemExNenhumPapelSelecionado));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC010_AcessarSiteSelecionando2ouMaisAvalista()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                //Nesse teste ele deve selecionar internamente desse método 2 ou mais papeis,
                //não consegui imaginar esse processo no automatizado
                SelecionarPapeisParaAssinatura(false, false, true);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemUploadOK));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        [TestMethod]
        public void TC011_AcessarSiteSelecionandoNenhumAvalistaMasClicandoNele()
        {
            try
            {
                op = "OPÇÃO 1";//Dado ficticio
                AcessarWebCRDC();
                AcessarMenuGestaoDeOperacoes(op);
                AnexarDocumento();
                //Nesse teste ele deve não selecionar internamente desse método os papeis,
                //não consegui imaginar esse processo no automatizado
                SelecionarPapeisParaAssinatura(false, false, true);
                ProcessoDeUpload();

                //Nesse processo fará a busca da mensagem
                //simulando que o id seja "mensagem"
                verificarMensagem = session.FindElement(By.Id("mensagem")).Text;
                Assert.IsTrue(verificarMensagem.Equals(mensagemExRepresentanteNaoCadastrado));
                FecharAPP();
            }
            catch
            {
                Assert.IsTrue("A".Equals("B"));//Sempre vai lançar erro
                FecharAPP();
            }
        }
        #endregion
    }
}
