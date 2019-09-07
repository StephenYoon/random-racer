import { Dice } from '../models/dice';

export interface DiceSet {
    display: string;
    description: string;
    dices: Dice[];
}
