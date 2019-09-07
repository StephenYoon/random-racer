import { PlayerStats } from "./playerStats";

export interface Player {
  id: number;
  firstname: string;
  lastname: string;
  email: string;
  stats : PlayerStats;
}