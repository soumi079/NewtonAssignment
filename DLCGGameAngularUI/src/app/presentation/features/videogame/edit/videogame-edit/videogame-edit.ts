import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { single } from 'rxjs/operators';
import { VideogameApiService } from '../../../../../core/infrastructure/videogame-api.service';
import { Videogame } from '../../../../../core/domain/models/videogame';

@Component({
  selector: 'app-videogame-edit',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './videogame-edit.html',
  styleUrl: './videogame-edit.scss',
})
export class VideogameEdit implements OnInit {  
  gameForm!: FormGroup; 

  constructor(
    private fb: FormBuilder, 
    private route: ActivatedRoute,
    private service: VideogameApiService,
    private router: Router
  ) {
    
    this.gameForm = this.fb.group({    
      id: [0], 
      title: ['', Validators.required],
      genre: ['Action', Validators.required],
      releaseYear: [2024, Validators.min(1950)],
      description: ['', Validators.maxLength(500)],
      price: [0, Validators.min(0)],
      rating: [0, [Validators.min(0), Validators.max(10)]],
    });
  }

  ngOnInit(): void {   
    const idParam = Number(this.route.snapshot.paramMap.get('id'));
   
    if (idParam && idParam !== 0) {      
      this.service.get(idParam).pipe(single()).subscribe((gameData: Videogame) => {        
        this.gameForm.patchValue(gameData);
      });
    }
  }

  save() {
    if (this.gameForm.valid) {
      const gameData = this.gameForm.value as Videogame;
      this.service.save(gameData).subscribe(() => this.router.navigate(['/games']));
      console.log('Saving game:', gameData);      
      
      this.router.navigate(['/games']);
    }    
  }

  goBack(){
    this.router.navigate(['/games']);
  }
}