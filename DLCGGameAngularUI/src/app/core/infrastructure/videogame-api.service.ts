import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Videogame } from '../domain/models/videogame';
import { PagedResult } from '../domain/interfaces/paged-result';

@Injectable({
  providedIn: 'root'
})
export class VideogameApiService {

  private baseUrl = 'https://localhost:7007/api/VideoGame';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Videogame[]> {
    return this.http.get<Videogame[]>(this.baseUrl);
  }
  
  get(id: number): Observable<Videogame> {
    return this.http.get<Videogame>(`${this.baseUrl}/${id}`);
  }

  save(game: Videogame) {
    if (!game.id || game.id === 0) {
      return this.http.post<Videogame>(this.baseUrl, game);
    } else {
      return this.http.put(`${this.baseUrl}/${game.id}`, game);
    }
  }

  getPaged(currentPage: number, pageSize: number): Observable<PagedResult<Videogame>> {
    let params = new HttpParams().set('currentPage', currentPage.toString()).set('pageSize', pageSize.toString());    
    return this.http.get<PagedResult<Videogame>>(this.baseUrl, { params });
  }

}