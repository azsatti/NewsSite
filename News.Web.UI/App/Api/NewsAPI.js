export default {
    getData: () => {
        return fetch('http://localhost:1311/api/news', {
            method: 'get'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    },
    create: (item) => {
        var data = JSON.stringify(item);
        return fetch('http://localhost:1311/api/news/', {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: data
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    }
}