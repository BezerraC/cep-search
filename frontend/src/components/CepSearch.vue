<template>
    <v-container>
        <h1 class="mb-4">Buscar CEP</h1>
        <v-row>
            <v-col cols="12" md="10">
                <v-text-field v-model="cep" label="Digite o CEP" :rules="[rules.required]" outlined
                    @keyup.enter="buscarCep" />
            </v-col>
            <v-col>
                <v-btn :disabled="loading" @click="buscarCep" color="teal">Buscar</v-btn>
            </v-col>
        </v-row>

        <v-progress-linear v-if="loading" color="teal" indeterminate></v-progress-linear>

        <v-row v-if="cepData" class="mb-5">
            <v-col cols="12">
                <v-card>
                    <v-card-title>Detalhes do CEP</v-card-title>
                    <v-divider></v-divider>
                    <v-card-text>
                        <v-row>
                            <v-col cols="12" sm="6" v-for="(value, key) in cepData" :key="key">
                                <p><strong>{{ formatKey(key) }}:</strong> {{ value || 'N/A' }}</p>
                            </v-col>
                        </v-row>
                    </v-card-text>
                </v-card>
            </v-col>
        </v-row>

        <v-row v-if="error" class="mt-3">
            <v-col cols="12">
                <v-alert type="error" closable @click:close="resetState">{{ error }}</v-alert>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
export default {
    name: 'CepSearch',
    data() {
        return {
            cep: "",
            cepData: null,
            error: null,
            rules: {
                required: (value) => !!value || "Este campo é obrigatório",
            },
            loading: false,
        };
    },
    methods: {
        async buscarCep() {
            if (!this.cep) return;
            this.error = null;
            this.loading = true;
            try {
                const response = await fetch(`http://localhost:5220/api/cep/${this.cep}`);
                if (!response.ok) {
                    this.error = "CEP não encontrado ou inválido";
                    this.cepData = null;
                    return;
                }
                this.cepData = await response.json();
                this.loading = false;
            } catch (err) {
                this.error = "Erro ao consultar a API";
                this.cepData = null;
            }
        },
        formatKey(key) {
            // Formata a chave para uma apresentação mais amigável
            return key
                .replace(/_/g, " ") // Substitui underscores por espaços
                .replace(/\b\w/g, (char) => char.toUpperCase()); // Capitaliza a primeira letra
        },
        resetState() {
            // Resetar todos os estados para o valor inicial
            this.cep = "";
            this.cepData = null;
            this.error = null;
            this.loading = false;
        },
    },
};
</script>

<style scoped>
.v-btn {
    height: 52px;
    width: 100%;
}
</style>