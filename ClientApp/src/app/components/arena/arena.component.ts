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

  private dice1: Dice;
  private dice2: Dice;
  private dice3: Dice;
  
  constructor() { }

  ngOnInit() {
  }

}
