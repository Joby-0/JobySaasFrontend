window.copyInviteCode = async function (button) {
    const code = button
        .closest('.invite-code-box')
        .querySelector('.invite-code')
        .textContent
        .trim();

    try {
        await navigator.clipboard.writeText(code);


        const icon = button.querySelector("i");
        icon.className = "bi bi-check";

        setTimeout(() => {
            icon.className = "bi bi-copy";
        }, 1500);

    } catch (error) {
        console.error("Copy failed:", error);
    }
};