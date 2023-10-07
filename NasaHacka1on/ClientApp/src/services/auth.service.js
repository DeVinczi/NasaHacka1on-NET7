interface CustomError {
    key: number;
    messages: string[];
}

class AuthService {
    #url = "/api/account/";

    async createAccount(user) {
        const url = this.#url + "sign-up";

        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(user),
        });

        const responseData = await response.json();


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

        const responseData = await response.json();

        return responseData;
    }
}

const authService = new AuthService();

export default authService;
