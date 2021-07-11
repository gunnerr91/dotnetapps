import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private httpClient: HttpClient) {}

  postQuestion(question: string): Observable<PostQuestionResponse> {
    return this.httpClient.post<PostQuestionResponse>(
      '/api/post-question',
      question
    );
  }
}

export interface PostQuestionResponse {
  status: number;
  message: string;
}
