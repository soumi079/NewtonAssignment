import { Routes } from '@angular/router';
import { VideogameList } from './presentation/features/videogame/list/videogame-list/videogame-list';
import { VideogameEdit } from './presentation/features/videogame/edit/videogame-edit/videogame-edit';

export const routes: Routes = [
    { path: 'games', component: VideogameList },
  { path: 'games/:id', component: VideogameEdit },
  { path: '', redirectTo: 'games', pathMatch: 'full' }
];
