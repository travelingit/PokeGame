import {Injectable} from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/do";
import "rxjs/add/observable/of";

@Injectable()
export class GameapiService{
    constructor(private _http:HttpClient){}

    getAllGamesInOrder(gen:string="", sort:string="releasedate",  dir:string="asc"):Observable<IGameRoot>{
        return this._http.get<IGameRoot>("http://localhost:16283/api/v1/games?page=0&gen=" + gen + "&sort=" + sort + "&dir=" + dir + "/" );
    }

    PostGame(gameToPost:IGameRoot){
        return this._http.post<IGameRoot>("http://localhost:16283/api/v1/games", gameToPost);
    }
    
    

}

interface IGameRoot {
    Id: number;
    Title: string;
    Releasedate: string;
    MyConsole: Iconsole;
    Generation: number;
    BoxImageUrl: string;
  }
  
  interface Iconsole {
    Id: number;
    Name: string;
    ImageUrl: string;
  }

