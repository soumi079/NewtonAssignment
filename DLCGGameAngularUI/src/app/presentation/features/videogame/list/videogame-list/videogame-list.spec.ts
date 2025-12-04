import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VideogameList } from './videogame-list';

describe('VideogameList', () => {
  let component: VideogameList;
  let fixture: ComponentFixture<VideogameList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VideogameList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VideogameList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
