var app = new Vue({
    el: '#app',
    data: {
      
        social_links: [
           
        ],
        about_me:"",
        about_image: '',
        about_image_alt_text: "About me",
     
        projects: [],
    },
    mounted() {
        fetch('https://gh-pinned-repos.egoist.sh/?username=sandip124')
            .then(res => res.json())
            .then(res => {
                for (const repo of res) {
                    this.projects.push({
                        project_icon: "./images/Github.svg",
                        project_title: repo.repo,
                        project_description: repo.description,
                        url: repo.link,
                        language: repo.language,
                        stars: repo.stars,
                        forks: repo.forks
                    })
                }
            });
    }
})


window.addEventListener('load', () => {
    let loader = document.getElementById('loader');
    setTimeout(() => {
        loader.style.display = "none";
    }, 2000);
});


