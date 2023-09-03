import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainNewComponent } from './train-new.component';

describe('TrainNewComponent', () => {
  let component: TrainNewComponent;
  let fixture: ComponentFixture<TrainNewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TrainNewComponent]
    });
    fixture = TestBed.createComponent(TrainNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
