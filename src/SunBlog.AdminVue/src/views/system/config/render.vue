<template>
	<div class="layout-padding" v-loading="vm.loading">
		<v-form-render ref="vfRenderRef"> </v-form-render>
		<el-row justify="center">
			<el-button type="primary" size="default" @click="onSubmit">保存</el-button>
		</el-row>
	</div>
</template>

<script setup lang="ts">
import { inject, onMounted, reactive, ref } from 'vue';
import { useRoute } from 'vue-router';
import { ElMessage } from 'element-plus';
import miitBus from '@/utils/mitt';
import { CustomConfigsServiceProxy, GetConfigDetailInput } from '@/shared/service-proxies';
const _customConfigService = new CustomConfigsServiceProxy(inject('$baseurl'), inject('$api'));
//路由
const route = useRoute();
const vfRenderRef = ref();
const vm = reactive({
	loading: true,
	formJson: {},
});
const onSubmit = async () => {
	try {
		const formData = await vfRenderRef.value?.getFormData();
		miitBus.emit('onCurrentContextmenuClick', Object.assign({}, { contextMenuClickId: 1, ...route }));
	} catch (e: any) {
		ElMessage.error(e);
	}
};
onMounted(async () => {
	vm.loading = true;
	const id = route.query.id as unknown;
	if (id !== undefined) {
		const { result } = await _customConfigService.getFormJson({ id: id as string } as GetConfigDetailInput);
		if (result) {
			vm.formJson = result.formJson ?? {};
			vfRenderRef.value?.setFormJson(vm.formJson);
		} else {
			ElMessage.error('请先配置设计');
			miitBus.emit('onCurrentContextmenuClick', Object.assign({}, { contextMenuClickId: 1, ...route }));
		}
	} else {
		miitBus.emit('onCurrentContextmenuClick', Object.assign({}, { contextMenuClickId: 1, ...route }));
		ElMessage.error('缺少参数');
	}
	vm.loading = false;
});
</script>

<style scoped></style>
