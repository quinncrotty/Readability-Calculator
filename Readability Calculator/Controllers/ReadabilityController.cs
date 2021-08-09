using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadabilityCalculator.Models.Readability;

namespace Readability_Calculator.Controllers
{
    public class ReadabilityController : Controller
    {
        // GET: ReadabilityController
        public ActionResult Index()
        {
            return View();
        }

       [HttpPost]

       public ActionResult Index(ReadabilityInput_vm model)
        {
            if (string.IsNullOrWhiteSpace(model.InputText))
            {
                ModelState.AddModelError("InputText", "Text cannot be empty or white space.");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ReadingScore readingScore = new ReadingScore();
            double readabilityScore = readingScore.Calculate(model.InputText);

            // Instantiate the ReadingScore class passing the model.InputText into the
            // class's constructor method. Then, perform a call to your instantiated
            // object's GetReadingScore() method. This method will calculate the score
            // and return that score back to you. Then, to obtain the Words, Sentences, and
            // Syllables simply reference the public properties of the same names from the
            // instantiated object.

            ReadabilityResults_vm viewModel = new ReadabilityResults_vm()
            {
                InputText = model.InputText,
                NumberWords = readingScore.words.ToString(),
                NumberSentences = readingScore.sentences.ToString(),
                NumberSyllables = readingScore.syllables.ToString(),
                ReadabilityScore = readabilityScore.ToString()
            };

            return View("ReadabilityResults", viewModel);

        }
    }
}
