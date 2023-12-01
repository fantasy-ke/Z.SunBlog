<template>
	<div class="system-render-dialog-container">
		<el-dialog title="编辑配置" v-model="state.isShowDialog" width="850px">
			<v-form-render ref="vfRenderRef" />
			<template #footer>
				<span class="dialog-footer">
					<el-button @click="onCancel" size="default">取 消</el-button>
					<el-button type="primary" @click="onSubmit" size="default">保存</el-button>
				</span>
			</template>
		</el-dialog>
	</div>
</template>

<script setup lang="ts">
import { reactive, ref, nextTick, inject } from 'vue';
import { ElMessage } from 'element-plus';
import {
	AddCustomConfigItemInput,
	CustomConfigItemsServiceProxy,
	CustomConfigsServiceProxy,
	GetConfigDetailInput,
	UpdateCustomConfigItemInput,
} from '@/shared/service-proxies';
const _customConfigService = new CustomConfigsServiceProxy(inject('$baseurl'), inject('$api'));
const _customConfigItemService = new CustomConfigItemsServiceProxy(inject('$baseurl'), inject('$api'));
// 定义子组件向父组件传值/事件
const emit = defineEmits(['refresh']);
// 表单渲染控件实例
const vfRenderRef = ref();
const state = reactive({
	formJson: {}, //表单渲染的json
	formData: {} as any, //表单数据绑定
	id: '',
	itemId: '',
	loading: false,
	isShowDialog: false,
	regex: /{"key":\d+,"type":"(file-upload|picture-upload)".*?"id".*?}/g, //匹配出json中的图片上传控件和附件上传控件
	fileOptions: [], //图片上传控件和附件上传控件
});
//取消
const onCancel = () => {
	state.isShowDialog = false;
};
//保存配置
const onSubmit = async () => {
	try {
		const formData = await vfRenderRef.value?.getFormData();
		//处理上传的附件和图片数据格式
		state.fileOptions.forEach((item: any) => {
			const field = formData[item.options.name];
			if (field && field.length > 0) {
				const urlList = field.filter((f: any) => f.response && f.response.length > 0).map((m: any) => m.response[0]);
				if (urlList.length > 0) {
					formData[item.options.name] = item.options.limit > 1 ? urlList.map((m: any) => m.url) : urlList[0].url;
				} else {
					formData[item.options.name] = null;
				}
			} else {
				formData[item.options.name] = null;
			}
		});
		console.log(state.itemId);

		const { success } = state.itemId
			? await _customConfigItemService.updateItem({
					json: JSON.stringify(formData),
					configId: state.id,
					id: state.itemId,
			  } as UpdateCustomConfigItemInput)
			: await _customConfigItemService.addItem({ json: JSON.stringify(formData), configId: state.id } as AddCustomConfigItemInput);
		if (success) {
			ElMessage.success('保存成功');
			state.isShowDialog = false;
			emit('refresh');
			return;
		}
	} catch (e: any) {
		ElMessage.error(e);
	}
};
// 打开弹窗
state.formJson = {};
const openDialog = async (id: string, itemId?: string) => {
	vfRenderRef.value?.resetForm();
	state.id = id;
	state.isShowDialog = true;
	state.loading = true;
	const { result, success, error } = await _customConfigService.getFormJson({ id: id, itemId: itemId } as GetConfigDetailInput);
	if (!success) {
		ElMessage.error(error.message);
		state.isShowDialog = false;
		return;
	}
	state.itemId = result?.itemId ?? '';
	state.formJson = result!.formJson!;
	const json = JSON.stringify(result!.formJson);
	const jsonString = json.match(state.regex)?.join(',');
	state.formData = result!.dataJson ?? {};
	if (jsonString) {
		state.fileOptions = JSON.parse(`[${jsonString}]`);
	}
	console.log(state.formData);

	if (result!.dataJson && state.fileOptions.length > 0) {
		//处理上传的附件和图片数据格式
		state.fileOptions.forEach((item: any) => {
			const field = state.formData[item.options.name];
			if (field && field.length > 0) {
				state.formData[item.options.name] =
					typeof field === 'string'
						? [
								{
									name: field.substring(field.lastIndexOf('/') + 1),
									url: field,
									response: [{ name: field.substring(field.lastIndexOf('/') + 1), url: field }],
								},
						  ]
						: field.map((m: string) => {
								return {
									name: m.substring(m.lastIndexOf('/') + 1),
									url: m,
									response: [{ name: m.substring(m.lastIndexOf('/') + 1), url: m }],
								};
						  });
			}
		});
	}
	// 渲染表单
	vfRenderRef.value?.setFormJson(state.formJson);
	nextTick(() => {
		let c = state.formData ? JSON.parse(state.formData) : {};
		console.log(c);

		// 绑定表单数据
		vfRenderRef.value?.setFormData(c);
	});
	state.loading = false;
};

//暴露方法
defineExpose({
	openDialog,
});
</script>

<style lang="scss" scoped></style>
