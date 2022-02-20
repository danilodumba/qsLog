import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdadePasswordComponent } from './updade-password.component';

describe('UpdadePasswordComponent', () => {
  let component: UpdadePasswordComponent;
  let fixture: ComponentFixture<UpdadePasswordComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdadePasswordComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdadePasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
