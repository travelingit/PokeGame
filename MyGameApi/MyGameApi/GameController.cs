﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGameApi
{

    [Route("api/v1/games")]
    public class GameController : Controller
    {
        private readonly GameLibraryContext context;
        public GameController(GameLibraryContext context)
        {
            this.context = context;
        }


        //alle games in een list
        [HttpGet]           // api/v1/games
        public List<Game> GetAllGames()
        {
            return context.Games.ToList();
        }


        //een specifieke game opvragen (via id)
        [Route("{id}")]     // api/v1/games/1
        [HttpGet]
        public IActionResult GetGameById(int id)
        {
            var showGame = context.Games.Find(id);
            if (showGame == null) return NotFound();
            return Ok(showGame);
        }


        //een specifieke game opvragen (via name)
        [Route("{name}")]     // api/v1/games/red&blue
        [HttpGet]
        public IActionResult GetGameByName(string name)
        {
            var showGame = context.Games.Find(name);
            if (showGame == null) return NotFound();
            return Ok(showGame);
        }


        //een nieuwe game aan de database toevoegen
        [HttpPost]
        public IActionResult CreateGame([FromBody] Game newGame)
        {
            context.Games.Add(newGame);
            context.SaveChanges();
            //stuur result 201 met de Game als content als het gelukt is
            return Created("", newGame);
        }


        //een game verwijderen (via id)
        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteGame(int id)
        {
            var deleteGame = context.Games.Find(id);
            if (deleteGame == null) return NotFound();

            //game verwijderen
            context.Games.Remove(deleteGame);
            context.SaveChanges();
            //stuur result 204 als het gelukt is
            return NoContent();
        }


        //een game aanpassen
        [HttpPut]
        public IActionResult UpdateGame([FromBody] Game updateGame)
        {
            var orgGame = context.Games.Find(updateGame.Id);
            if (orgGame == null) return NotFound();

            orgGame.Title = updateGame.Title;
            orgGame.Releasedate = updateGame.Releasedate;
            orgGame.MyConsole = updateGame.MyConsole;
            orgGame.BoxImageUrl = updateGame.BoxImageUrl;
            orgGame.Generation = updateGame.Generation;

            context.SaveChanges();
            return Ok(orgGame);
        }
    }
}
