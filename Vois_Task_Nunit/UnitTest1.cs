using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Vois_Task_Nunit
{
    public class Tests
    {
        IWebDriver driver;
        string path = "C:\\Users\\Mohamed Tharwat\\Downloads\\chromedriver_win32";
        String url = "http://automationpractice.com/index.php";

        //Test Data
        string testMail = "bikafor778@procowork.com";
        string fname = "Muhamed";
        string lname = "Tharwat";
        string passwd = "Tharwat@CCC";
        string stname1 = "shobra";
        string stname2 = "St";
        string company = "3s";
        string address = "Cairo";
        string city = "Cairo";
        string pcode = "11311";
        string mobPhone = "01123456788";
        string alias = "egypt";
        string stateName = "Alabama";

        string orderReferance;
        string orderQuantity;
        string orderNetTotalPrice;
        string orderShippingCompany;
        string orderShippingCost;
        string orderTotalAfterTax;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@path);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = url;
        }

        [Test]
        public void authenticateUser()
        {
            IWebElement signInBtn = driver.FindElement(By.ClassName("login"));
            signInBtn.Click();

            IWebElement createEmailTxt = driver.FindElement(By.Id("email_create"));
            createEmailTxt.SendKeys(testMail);
            IWebElement createEmailBtn = driver.FindElement(By.CssSelector("#SubmitCreate"));
            createEmailBtn.Click();

            //to handle multiple runs with different mails as email can not be duplicated
            Thread.Sleep(4000);
            IWebElement alert = driver.FindElement(By.Id("create_account_error"));
            if (alert.Displayed)
            {
                Random rnd = new Random();
                int num = rnd.Next(200);    //genrate random number
                testMail = string.Concat("AC" + num, testMail); //generat a new test mail
                createEmailTxt.Clear();
                createEmailTxt.SendKeys(testMail);
                createEmailBtn.Click();
            }


            IWebElement createform = driver.FindElement(By.Id("account-creation_form"));
            IList<IWebElement> inputs = createform.FindElements(By.TagName("input"));   //list of all input fields in Create user form
            if (!inputs[0].Selected)
            {
                inputs[0].Click();
            }
            inputs[2].SendKeys(fname);
            inputs[3].SendKeys(lname);
            if (inputs[4].Text != testMail)
            {
                inputs[4].Clear();
                inputs[4].SendKeys(testMail);
            }
            inputs[5].SendKeys(passwd);
            inputs[8].Clear();
            inputs[8].SendKeys(stname1);
            inputs[9].Clear();
            inputs[9].SendKeys(stname2);
            inputs[10].SendKeys(company);
            inputs[11].SendKeys(address);
            inputs[13].SendKeys(city);
            inputs[14].SendKeys(pcode);
            inputs[16].Clear();
            inputs[16].SendKeys(mobPhone);
            inputs[17].Clear();
            inputs[17].SendKeys(alias);

            SelectElement state = new SelectElement(driver.FindElement(By.Id("id_state")));
            state.SelectByText(stateName);

            IWebElement registerBtn = driver.FindElement(By.CssSelector("#submitAccount > span"));
            registerBtn.Click();
        }

        [Test]
        public void purchaseOrder()
        {
            //sign with registered mail
            IWebElement signInBtn = driver.FindElement(By.ClassName("login"));
            signInBtn.Click();
            IWebElement EmailTxt = driver.FindElement(By.Id("email"));
            EmailTxt.SendKeys(testMail);
            IWebElement passwdTxt = driver.FindElement(By.Id("passwd"));
            passwdTxt.SendKeys(passwd);
            IWebElement signBtn = driver.FindElement(By.CssSelector("#SubmitLogin > span"));
            signBtn.Click();


            //Select “Blouses” Subcategory in “Women” Category
            IWebElement womenTab = driver.FindElement(By.ClassName("sf-with-ul"));
            Actions a = new Actions(driver);
            a.MoveToElement(womenTab).Perform();
            IWebElement cateBtn = driver.FindElement(By.CssSelector("[title='Blouses']"));
            cateBtn.Click();
            IWebElement addCartBtn = driver.FindElement(By.CssSelector("[title='Add to cart'] > span"));
            addCartBtn.Click();

            //get order details to validate with in order history
            IWebElement quantity = driver.FindElement(By.Id("layer_cart_product_quantity"));
            orderQuantity = quantity.Text;
            IWebElement netTotalPrice = driver.FindElement(By.Id("layer_cart_product_price"));
            orderNetTotalPrice = netTotalPrice.Text;

            IWebElement proceed = driver.FindElement(By.CssSelector("[title='Proceed to checkout'] > span"));
            proceed.Click();

            //get order details to validate with in order history
            IWebElement referance = driver.FindElement(By.CssSelector(".cart_ref"));
            orderReferance = referance.Text;

            IWebElement proceed2 = driver.FindElement(By.CssSelector(".standard-checkout > span"));
            proceed2.Click();

            IWebElement proceed3 = driver.FindElement(By.CssSelector("[name='processAddress'] > span"));
            proceed3.Click();

            //get order details to validate with in order history
            IWebElement shippingCompany = driver.FindElement(By.CssSelector(".resume > tbody > tr > td:nth-of-type(3) > strong"));
            orderShippingCompany = shippingCompany.Text;
            IWebElement shippingCost = driver.FindElement(By.CssSelector(".resume > tbody > tr > td:nth-of-type(4) > div"));
            orderShippingCost = shippingCost.Text;

            IWebElement agreedTerms = driver.FindElement(By.Id("cgv"));
            agreedTerms.Click();
            IWebElement proceed4 = driver.FindElement(By.CssSelector("[name='processCarrier'] > span"));
            proceed4.Click();

            IWebElement bankWire = driver.FindElement(By.CssSelector("[title='Pay by bank wire']"));
            bankWire.Click();

            //get order details to validate with in order history
            IWebElement totalAfterTax = driver.FindElement(By.Id("amount"));
            orderTotalAfterTax = totalAfterTax.Text;

            IWebElement confirmBtn = driver.FindElement(By.CssSelector("#cart_navigation .button > span"));
            confirmBtn.Click();
        }
        [Test]
        public void verfiyOrder()
        {
            //sign with registered mail
            IWebElement signInBtn = driver.FindElement(By.ClassName("login"));
            signInBtn.Click();
            IWebElement EmailTxt = driver.FindElement(By.Id("email"));
            EmailTxt.SendKeys(testMail);
            IWebElement passwdTxt = driver.FindElement(By.Id("passwd"));
            passwdTxt.SendKeys(passwd);
            IWebElement signBtn = driver.FindElement(By.CssSelector("#SubmitLogin > span"));
            signBtn.Click();

            IWebElement viewAccBtn = driver.FindElement(By.CssSelector("[title='View my customer account'] > span"));
            viewAccBtn.Click();

            IWebElement openOrderHistory = driver.FindElement(By.CssSelector("[title='Orders'] > span"));
            openOrderHistory.Click();

            IWebElement viewOrder = driver.FindElement(By.CssSelector("#order-list > tbody > tr > td > a"));
            viewOrder.Click();

            IWebElement referance = driver.FindElement(By.CssSelector("#order-detail-content .table > tbody > tr > td > label"));
            IWebElement quantity = driver.FindElement(By.CssSelector(".order_qte_span"));
            IWebElement totalPrice = driver.FindElement(By.CssSelector("#order-detail-content .table > tfoot > tr > td:nth-of-type(2) > span"));
            IWebElement totalAfterTax = driver.FindElement(By.CssSelector(".totalprice .price"));
            IWebElement shippingComoany = driver.FindElement(By.CssSelector("#block-order-detail .footab > tbody > tr > td:nth-of-type(2)"));
            IWebElement shippingCost = driver.FindElement(By.CssSelector("#block-order-detail .footab > tbody > tr > td:nth-of-type(4)"));

            
            Assert.AreEqual(orderQuantity, quantity.Text);
            Assert.AreEqual(orderNetTotalPrice, totalPrice.Text);
            Assert.AreEqual(orderTotalAfterTax, totalAfterTax.Text);
            Assert.AreEqual(orderShippingCompany, shippingComoany.Text);
            Assert.AreEqual(orderShippingCost, shippingCost.Text);



        }

    }
}