/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./**/*.{razor,html}'],
    safelist: [
        {
            pattern: /grid-cols-+/
        },
        {
            pattern: /col-span-+/
        },
        {
            pattern: /bg-+/
        },
        {
            pattern: /from-+/
        },
        {
            pattern: /to-+/
        }
    ],
    theme: {
        container: {
            center: true,
            padding: "2rem"
        },
    extend: {},
  },
  plugins: [],
}

