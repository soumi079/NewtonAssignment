import { Component, OnInit,signal, WritableSignal, computed  } from '@angular/core';
import { Videogame } from '../../../../../core/domain/models/videogame';
import { VideogameApiService } from '../../../../../core/infrastructure/videogame-api.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { catchError, finalize, of } from 'rxjs';
import { PagedResult } from '../../../../../core/domain/interfaces/paged-result';
import { debug } from 'console';

@Component({
  selector: 'app-videogame-list',
  standalone: true,
  imports: [],
  templateUrl: './videogame-list.html',
  styleUrl: './videogame-list.scss',
})
export class VideogameList implements OnInit {
  readonly DEFAULT_PAGE_SIZE: number = 5;
  // Use signals for reactive state management
  gameData: WritableSignal<PagedResult<Videogame> | null> = signal(null);
  totalCount: WritableSignal<number> = signal(1);
  currentPage: WritableSignal<number> = signal(1);
  pageSize: WritableSignal<number> = signal(this.DEFAULT_PAGE_SIZE);
  totalPages: WritableSignal<number> = signal(1);
  
  isLoading: WritableSignal<boolean> = signal(false);
  errorMessage: WritableSignal<string | null> = signal(null);

  constructor(private svc: VideogameApiService, private router: Router) {}

  ngOnInit(): void {
    this.reload();    
  }

  reload(): void {
    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.svc.getPaged(this.currentPage(), this.pageSize(), "", "", false).pipe(
      catchError(error => {
        console.error('API Error:', error);
        this.errorMessage.set('Failed to fetch data. Please try again.');
        this.gameData.set(null);
        return of(null);
      }),
      finalize(() => {
        this.isLoading.set(false);
      })
    ).subscribe(data => {
      if (data) {
        this.totalPages.set(data.totalPages);
        this.totalCount.set(data.totalItemCount);
        this.currentPage.set(data.currentPage);
        this.gameData.set(data);
      }
    });
  }
   
  onPrevPage(): void {
    const prevPage = this.currentPage() - 1;       

    if (this.gameData() && prevPage < 1) {
      console.log('Already on the first page.');
      return;
    }
    
    this.currentPage.set(prevPage);
    this.reload();
  }

  isFirstDisabled(): boolean {
    const data = this.gameData();
    if (!data || this.isLoading()) {
      return true;
    }
    return this.currentPage() <= 1;
  }

  onNextPage(): void {
    debugger;
    const nextPage = this.currentPage() + 1;
     

    if (this.gameData() && nextPage > this.totalPages()) {
      console.log('Already on the last page.');
      return;
    }
    
    this.currentPage.set(nextPage);
    this.reload();
  } 

  isNextDisabled(): boolean {
    const data = this.gameData();
    if (!data || this.isLoading()) {
      return true;
    }
    return this.currentPage() >= this.totalPages();
  }

  edit(game: Videogame) {
    this.router.navigate(['/games', game.id]);
  }

  add() {
    this.router.navigate(['/games/new']);
  }
}
