export class Contact {
  id: string;
  categoryId: string;
  name: string;
  email: string;
  birthDate: Date;

  constructor(categoryId: string) {
    this.categoryId = categoryId;
  }
}
