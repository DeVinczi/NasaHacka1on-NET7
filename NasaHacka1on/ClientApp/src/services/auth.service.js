class AuthService {
  #url = "/api/account/";

  async createAccount({ displayName, email, password }) {
    const url = this.#url + "sign-up";

    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        displayName,
        email,
        password,
      }),
    });

    const responseData = await response.json();
    console.log(responseData);
  }
}

export const httpService = new AuthService();
