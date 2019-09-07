import { Component, OnInit } from '@angular/core';
import { Dice } from '../../models/dice';
import { DiceSet } from '../../models/diceSet';
import { DiceSetCollection } from '../../data/diceSetCollection'

@Component({
  selector: 'app-arena',
  templateUrl: './arena.component.html',
  styleUrls: ['./arena.component.scss']
})
export class ArenaComponent implements OnInit {

  public playerDiceSet: Dice[];

  private diceSetCollection: DiceSetCollection = new DiceSetCollection;
  
  constructor() { }

  ngOnInit() {
    this.playerDiceSet = [
      this.diceSetCollection['regular'].dices[0],      
      this.diceSetCollection['regular'].dices[1],
      this.diceSetCollection['regular'].dices[2]
    ];    
  }
}
