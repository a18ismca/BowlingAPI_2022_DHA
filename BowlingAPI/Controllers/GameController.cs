using BowlingAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BowlingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IConfiguration _config;

        //The frames are stored here.
        List<Frame> frames = new List<Frame>(10);
     

        public GameController(IConfiguration config)
        {
            _config = config;
        }

        // The following method is never used.
        public int Sum(List<Frame> frames)
        {
            int summary = 0;
            foreach(Frame frame in frames)
            {
                summary = frame.Sum;
            }
            return summary;
        }

        [HttpGet]
        public JsonResult Get()
        {
            
            // Generate a random number.
            var rolls = new Random();

            

            // A game has 10 frames.

            for (int i = 0; i <= 9; i++)
            {
                var firstThrow = rolls.Next(0, 11);

                // We want to calculate how many pins are left after the first throw, unless it is a strike.

                var secondThrow = rolls.Next(0, 11 - firstThrow);
                var total = firstThrow + secondThrow;
                frames.Add(new Frame
                {
                    FrameNumber = i,
                    FirstScore = firstThrow,
                    SecondScore = secondThrow,
                    Sum = total

                }
                ); 
            }           
            return new JsonResult(frames);
        }

       

    }
}
