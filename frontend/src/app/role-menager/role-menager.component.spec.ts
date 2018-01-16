import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RoleMenagerComponent } from './role-menager.component';

describe('RoleMenagerComponent', () => {
  let component: RoleMenagerComponent;
  let fixture: ComponentFixture<RoleMenagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RoleMenagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RoleMenagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
