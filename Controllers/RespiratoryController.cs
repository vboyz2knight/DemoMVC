using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMVC.Respiratory;
using DemoMVC.Classes;
using Ninject;
using DemoMVC.Models;
using SimpleMathExpression;

namespace DemoMVC.Controllers
{
    public class RespiratoryController : Controller
    {
        private ICacheProvider Cache;
        private IReadEquationData<Equation> ReadEquationData;
        private IMathExpressionParser MathExpressionParser;
        //private IMyLogger logger;

        public RespiratoryController([Named("FileDependency")]ICacheProvider cache, IReadEquationData<Equation> readEquationData,IMathExpressionParser mathExpressionParser)
        {
            //this.logger = Logger;
            this.Cache = cache;
            this.ReadEquationData = readEquationData;
            this.MathExpressionParser = mathExpressionParser;
        }
        public ActionResult ACLSNotes()
        {
            return View();
        }
        // GET: Respiratory
        public ActionResult TankDuration()
        {
            TankDurationViewModel model = new TankDurationViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult TankDuration(TankDurationViewModel input_model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Duration(in minutes) = (gauge_pressure(psi) * tank_factor) / liter_flow
                    EquationsRepository equationRespository = new EquationsRepository(Cache, ReadEquationData);
                    Equation requestedEquation = equationRespository.GetAnEquation("Tank_Oxygen_Duration");

                    if (requestedEquation != null)
                    {
                        string myEquationExpression = requestedEquation.EquationExpression;
                        double myAnswer = 0;
                        string error = string.Empty;

                        //Substitute my variable in equation with value from user.

                        myEquationExpression = myEquationExpression.Replace("tank_factor", input_model.selectedTankFactor.ToString());
                        myEquationExpression = myEquationExpression.Replace("gauge_pressure_psi", input_model.gauge_pressure_psi.ToString());
                        myEquationExpression = myEquationExpression.Replace("liter_flow_L", input_model.liter_flow_L.ToString());


                        if(MathExpressionParser.SolveExpression(myEquationExpression,out myAnswer,out error))
                        {
                            input_model.myAnswer = myAnswer;
                        }
                        else
                        {
                            //logger.Error("Error using SimpleCalculatorService.", e);
                            return View("Error");
                        }
                        //DemoMVC.SimpleCalcServiceConsoleHost.CalculatorClient calculatorClient = new SimpleCalcServiceConsoleHost.CalculatorClient();

                        //try
                        //{
                        //    myAnswer = calculatorClient.SolveExpression(myEquationExpression);
                        //    input_model.myAnswer = double.Parse(myAnswer);
                        //    // More code that isn't useful including here ...
                            
                        //}
                        //catch (Exception e)
                        //{
                        //    ErrorSignal.FromCurrentContext().Raise(e);
                        //    //logger.Error("Error using SimpleCalculatorService.", e);
                        //}
                        //finally
                        //{
                        //    calculatorClient.Close();
                        //}
                    }

                }
                catch (Exception ex)
                {
                    //ErrorSignal.FromCurrentContext().Raise(ex);
                }
            }


            return View(input_model);
        }

        public ActionResult ListRespiratoryEquations()
        {
            EquationsRepository equationRespository = new EquationsRepository(Cache, ReadEquationData);

            List<Equation> equationList =  equationRespository.GetAllEquations();

            return View(equationList);
        }
        public ActionResult EquationGeneratorForm(string abbreviation)
        {
            EquationsRepository equationRespository = new EquationsRepository(Cache, ReadEquationData);
            Equation requestedEquation = equationRespository.GetAnEquation(abbreviation);
            EquationAnswerViewModel equationAnswerViewModel = new EquationAnswerViewModel();
            if(requestedEquation!=null)
            {
                equationAnswerViewModel.myEquation = requestedEquation;
            }

            return View(equationAnswerViewModel);
        }

        [HttpPost]
        public ActionResult EquationGeneratorForm(FormCollection collection)
        {
            string equationQuery = string.Empty;
            string equation = string.Empty;
            double answer = 0;
            string error = string.Empty;

            EquationAnswerViewModel equationAnswerViewModel = new EquationAnswerViewModel();            
            EquationsRepository equationRespository = new EquationsRepository(Cache, ReadEquationData);

            if (collection.AllKeys.Contains("abbreviation"))
            {
                equationQuery = collection["abbreviation"];

                Equation requestedEquation = equationRespository.GetAnEquation(equationQuery);

                if (requestedEquation != null)
                {
                    equation = requestedEquation.EquationExpression;
                    equationAnswerViewModel.myEquation = requestedEquation;
                }

                //Substitute my variable in equation with value from user.
                foreach (string key in collection.Keys)
                {
                    string tmp = collection[key];

                    if (equation.Contains(key))
                    {
                        equation = equation.Replace(key, tmp);
                        if (equationAnswerViewModel.myCollection==null)
                        {
                            equationAnswerViewModel.myCollection = new Dictionary<string, string>();
                        }
                        equationAnswerViewModel.myCollection.Add(key, tmp);
                    }
                }
                

                if (MathExpressionParser.SolveExpression(equation, out answer, out error))
                {
                    equationAnswerViewModel.myAnswer = answer;
                }
                else
                {
                    //logger.Error("Error using SimpleExpressionParser {0}.", error);
                    return View("Error");
                }

                //DemoMVC.SimpleCalcServiceConsoleHost.CalculatorClient calculatorClient = new SimpleCalcServiceConsoleHost.CalculatorClient();
                
                
                //try
                //{
                //    answer = calculatorClient.SolveExpression(equation);
                //    equationAnswerViewModel.myAnswer = double.Parse(answer);
                //    // More code that isn't useful including here ...
                    
                //}
                //catch (Exception e)
                //{
                //    ErrorSignal.FromCurrentContext().Raise(e);
                //    //logger.Error("Error using SimpleCalculatorService.", e);
                //}
                //finally
                //{
                //    calculatorClient.Close();
                //}


            }
            else
            {
                throw new ArgumentNullException("Key not found.");
            }

            return View(equationAnswerViewModel);
        }
    }
}