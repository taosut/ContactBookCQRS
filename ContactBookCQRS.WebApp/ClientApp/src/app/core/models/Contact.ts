export class Contact {
  id: string;
  categoryId: string;
  name: string;
  email: string;
  birthDate: Date;
  phoneNumber: string;

  constructor(categoryId: string) {
    this.categoryId = categoryId;
  }
}
