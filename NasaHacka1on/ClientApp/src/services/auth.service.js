class AuthService {
  #url = "https://localhost:7120/api/account/";

  async createAccount(user) {
    const url = this.#url + "sign-up";

    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    });
    console.log(response);

    const responseData = await response.json();
    console.log(responseData);

    return responseData;
  }

  async login(user) {
    const url = this.#url + "sign-in";

    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(user),
    });

    console.log(response);

    const responseData = await response.json();
    console.log(responseData);

    return responseData;
  }
}

const authService = new AuthService();

export default authService;
