using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XXXVideoPicker.commands
{
    public class VideoPickerCommands : BaseCommandModule
    {
        [Command("pr")]
        public async Task VideoPickingRandom(CommandContext ctx, string category, int pageNumber = 1)
        {
            if (pageNumber > 455)
            {
                await ctx.Channel.SendMessageAsync("https://www.youtube.com/watch?v=igicCPUY-vo&t=1s");
            }
            else
            {
                EdgeOptions edgeOptions = new EdgeOptions();
                edgeOptions.AddArgument("headless");
                edgeOptions.AddArgument("disable-gpu");
                IWebDriver driver = new EdgeDriver(edgeOptions);
                string pornPath = ("https://www.pornhub.com/video/search?search=" + category + "&page=" + pageNumber);
                driver.Navigate().GoToUrl(pornPath);
                Thread.Sleep(500);
                IWebElement loginBtn = driver.FindElement(By.XPath("//*[@id=\"modalWrapMTubes\"]/div/div/button"));
                loginBtn.Click();

                Thread.Sleep(1000);
                var pornVideos = driver.FindElements(By.ClassName("gtm-event-thumb-click"));
                List<String> videos = new List<string>();
                foreach (var v in pornVideos)
                {
                    if(v.GetAttribute("href")!= "javascript:void(0)")
                    {
                        videos.Add(v.GetAttribute("href"));
                        Console.WriteLine(v.GetAttribute("href"));
                    }
                    else
                    {
                        continue;
                    }
                }
                driver.Close();
                Random rng = new Random();
                int randomVideo = rng.Next(5, 36);

                await ctx.Channel.SendMessageAsync(videos[randomVideo]);
                videos.Clear();
            }
        }

        [Command("pmv")]
        public async Task VideoPickingMv(CommandContext ctx, string category, int pageNumber = 1)
        {
            if (pageNumber > 455)
            {
                await ctx.Channel.SendMessageAsync("https://www.youtube.com/watch?v=igicCPUY-vo&t=1s");
            }
            else
            {
                EdgeOptions edgeOptions = new EdgeOptions();
                edgeOptions.AddArgument("headless");
                edgeOptions.AddArgument("disable-gpu");
                IWebDriver driver = new EdgeDriver(edgeOptions);
                string pornPath = ("https://www.pornhub.com/video/search?search=" + category + "&o=mv&page=" + pageNumber);
                driver.Navigate().GoToUrl(pornPath);
                Thread.Sleep(500);
                IWebElement loginBtn = driver.FindElement(By.XPath("//*[@id=\"modalWrapMTubes\"]/div/div/button"));
                loginBtn.Click();

                Thread.Sleep(1000);
                var pornVideos = driver.FindElements(By.ClassName("gtm-event-thumb-click"));
                List<String> videos = new List<string>();
                foreach (var v in pornVideos)
                {
                    videos.Add(v.GetAttribute("href"));
                    Console.WriteLine(v.GetAttribute("href"));
                }
                driver.Close();

                await ctx.Channel.SendMessageAsync(videos[4]);
                videos.Clear();
            }
        }

        [Command("phelp")]
        public async Task BotHelp(CommandContext ctx)
        {
            var helpMessage = new DiscordEmbedBuilder
            {
                Title = "XXX Video Picker Commands",
                Description = "!pr 'Category' 'Page Number' <- For picking random video\n!pmv 'Category' 'Page Number' <- For picking most viewed video",
                Color = DiscordColor.Orange
            };

            await ctx.Channel.SendMessageAsync (embed : helpMessage);
        }
    }
}
