import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VideogameEdit } from './videogame-edit';

describe('VideogameEdit', () => {
  let component: VideogameEdit;
  let fixture: ComponentFixture<VideogameEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VideogameEdit]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VideogameEdit);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
