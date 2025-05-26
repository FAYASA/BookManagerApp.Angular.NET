import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { provideHttpClient, withFetch } from '@angular/common/http';



interface Book {
  id?: number;
  title: string;
  author: string;
  isbn: string;
  publicationDate: string;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  books: Book[] = [];

  book: Book = {
    title: '',
    author: '',
    isbn: '',
    publicationDate: ''
  };

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getBooks();
  }

  getBooks() {
    this.http.get<Book[]>('https://localhost:7198/api/Books')
      .subscribe(data => {
        this.books = data;
      });
  }

  onSubmit() {
    this.http.post<Book>('https://localhost:7198/api/Books', this.book)
      .subscribe(() => {
        this.book = {
          title: '',
          author: '',
          isbn: '',
          publicationDate: ''
        };
        this.getBooks();  // reload books after adding
      });
  }

  deleteBook(id: number | undefined) {
    if (!id) return;

    this.http.delete(`https://localhost:7198/api/Books/${id}`)
      .subscribe(() => {
        this.getBooks();  // reload books after deleting
      });
  }
}

