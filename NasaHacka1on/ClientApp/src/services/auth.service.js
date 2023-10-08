const clearCookie = (name) => {
    document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/`;
};

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

    async logout() {
        const url = this.#url + "sign-out";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(),
        });
        
        return;
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
