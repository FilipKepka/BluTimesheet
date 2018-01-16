import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectMenagerComponent } from './project-menager.component';

describe('ProjectMenagerComponent', () => {
  let component: ProjectMenagerComponent;
  let fixture: ComponentFixture<ProjectMenagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProjectMenagerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProjectMenagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
