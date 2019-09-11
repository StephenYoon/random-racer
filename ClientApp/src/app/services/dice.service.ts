import { Injectable } from '@angular/core';
import { Dice } from '../models/dice';
import { DiceSet } from '../models/diceSet';
import { DiceSetCollection } from '../data/diceSetCollection'
import { Player } from '../models/player';
import { PlayerStats } from '../models/playerStats';

@Injectable({
  providedIn: 'root'
})
export class DiceService {

  constructor() { }

  getPlayer(id: number): Player {
    let player: Player = new Player;
    player.id = 1;
    player.email = "whoami@gmail.com"
    player.firstname = "who";
    player.lastname = "ami"
    player.stats = new PlayerStats;
    player.stats.wins = 99;
    player.stats.loses = 1;
    player.stats.draw = 0;
    return player;
  }
  
  calculateDiceRoll(diceSetKey: string): Dice {
    let diceSet: Dice = DiceSetCollection[diceSetKey];
    let randomIndex = 1; // TODO: this is not very random, update this
    return diceSet[randomIndex];
  }

  getDiceImagePrefix(diceSetKey: string): string {
    let imagePrefix: string;
    switch(diceSetKey) { 
      case 'regular': {
        imagePrefix = diceSetKey;
        break; 
      }
      case 'boosted': {
        imagePrefix = diceSetKey;
        break;
      }
      case 'notGood': {
        imagePrefix = diceSetKey;
        break;
      }
      case 'extraGoodness': {
        imagePrefix = diceSetKey;
        break;
      }
      default: {
        imagePrefix = diceSetKey;
        break;
      }
    }

    return imagePrefix;
  }
}
