import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainViewComponent } from './train-view.component';

describe('TrainViewComponent', () => {
  let component: TrainViewComponent;
  let fixture: ComponentFixture<TrainViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TrainViewComponent]
    });
    fixture = TestBed.createComponent(TrainViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
