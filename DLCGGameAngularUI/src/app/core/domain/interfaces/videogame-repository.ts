import { Videogame } from "../models/videogame";

export interface VideogameRepository {
    getAll(): Promise<Videogame[]>;
    getById(id:number): Promise<Videogame|null>;
}
