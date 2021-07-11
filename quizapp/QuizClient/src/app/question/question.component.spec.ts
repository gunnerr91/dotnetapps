import {
  ComponentFixture,
  fakeAsync,
  TestBed,
  tick,
} from '@angular/core/testing';

import { QuestionComponent } from './question.component';
import { FormsModule } from '@angular/forms';
import { By } from '@angular/platform-browser';

describe('QuestionComponent', () => {
  let component: QuestionComponent;
  let fixture: ComponentFixture<QuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [QuestionComponent],
      imports: [FormsModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('when clicking post button should call post function', fakeAsync(() => {
    spyOn(component, 'post');

    let button =
      fixture.debugElement.nativeElement.querySelector('#post-button');
    button.click();
    tick();
    expect(component.post).toHaveBeenCalled();
  }));

  it('updates question when post is clicked', async () => {
    await fixture.whenStable();
    let button =
      fixture.debugElement.nativeElement.querySelector('#post-button');
    let inputField = fixture.debugElement.query(By.css('#question-input'));
    let inputElement = inputField.nativeElement;
    inputElement.value = 'this is the question';
    inputElement.dispatchEvent(new Event('input'));
    button.click();
    expect(component.question).toBe('this is the question');
  });
});
