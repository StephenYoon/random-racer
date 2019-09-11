import { Component, OnInit } from '@angular/core';
import { Dice } from '../../models/dice';
import { DiceSet } from '../../models/diceSet';
import { DiceSetCollection } from '../../data/diceSetCollection'
import { DiceService } from '../../services/dice.service'
import { Player } from 'src/app/models/player';

@Component({
  selector: 'app-arena',
  templateUrl: './arena.component.html',
  styleUrls: ['./arena.component.scss']
})
export class ArenaComponent implements OnInit {

  public player: Player;
  public playerDiceSet: Dice[];
  public diceSetKey: string = "regular";

  private diceService: DiceService;
  private diceSetCollection: DiceSetCollection = new DiceSetCollection;
  
  constructor(diceService: DiceService) {
    this.diceService = diceService;
  }

  ngOnInit() {
    this.playerDiceSet = [
      this.diceSetCollection[this.diceSetKey].dices[0],      
      this.diceSetCollection[this.diceSetKey].dices[1],
      this.diceSetCollection[this.diceSetKey].dices[2]
    ];

    this.player = this.diceService.getPlayer(1);
  }

  getPlayerDiceSet(): Dice[] {
    return this.playerDiceSet;
  }

  // Get a new set of dice
  // TODO: opportunities to refactor this below, but for now it's "okay"
  rollDice(dices: Dice[]) {
    for (let i = 0; i < this.playerDiceSet.length; i++) {
      let randomIndex = this.randomIntFromInterval(1, 6);
      this.playerDiceSet[i] = this.diceSetCollection[this.diceSetKey].dices[randomIndex - 1]
    }
  }

  // Random number generator, min and max included.
  randomIntFromInterval(min, max): number {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }
}
